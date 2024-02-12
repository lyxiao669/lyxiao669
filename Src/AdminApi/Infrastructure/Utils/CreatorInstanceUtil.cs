using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure
{
    public class CreatorInstanceUtil
    {
        private static ConcurrentDictionary<Type, Func<Dictionary<string, object>, object>> _creator = new ConcurrentDictionary<Type, Func<Dictionary<string, object>, object>>();

        private static ConcurrentDictionary<Type, Action<object, Dictionary<string, object>>> _deconstructions
            = new ConcurrentDictionary<Type, Action<object, Dictionary<string, object>>>();

        public static Action<object, Dictionary<string, object>> CreateDeconstructionsHandler(Type type)
        {
            var dictionaryType = typeof(Dictionary<string, object>);
            var dictionaryAddMethod = dictionaryType.GetMethod(nameof(Dictionary<string, object>.Add), BindingFlags.Public | BindingFlags.Instance);
            var parameter1 = Expression.Parameter(typeof(object), "arg1");
            var parameter2 = Expression.Parameter(typeof(Dictionary<string, object>), "arg2");
            var argurment = Expression.TypeAs(parameter1, type);
            var blocks = new List<Expression>();
            foreach (var member in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var key = Expression.Constant(char.ToLower(member.Name[0]) + member.Name[1..]);
                var value = Expression.TypeAs(Expression.Property(argurment, member), typeof(object));
                var call = Expression.Call(parameter2, dictionaryAddMethod, key, value);
                blocks.Add(call);
            }
            var body = Expression.Block(blocks);
            var lambda = Expression.Lambda(body, parameter1, parameter2);
            return lambda.Compile() as Action<object, Dictionary<string, object>>;
        }

        public static Action<object, Dictionary<string, object>> GetDeconstructionsHandler(Type type)
        {
            return _deconstructions.GetOrAdd(type, t =>
            {
                return CreateDeconstructionsHandler(type);
            });
        }

        public static ConstructorInfo GetConstructor(Type type, string[] names)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                if (!constructor.IsPublic)
                {
                    continue;
                }
                var parameters = constructor.GetParameters();
                if (parameters.Length > names.Length)
                {
                    continue;
                }
                if (parameters.All(a => names.Contains(a.Name)))
                {
                    return constructor;
                }
            }
            throw new Exception("找不到匹配的构造函数");
        }

        public static Func<Dictionary<string, object>, object> GetCreatorHandler(Type type, Dictionary<string, object> args)
        {
            return _creator.GetOrAdd(type, t =>
            {
                var constructor = GetConstructor(type, args.Select(s => s.Key).ToArray());
                var arg0 = Expression.Parameter(typeof(Dictionary<string, object>), "arg0");
                var parameters = constructor.GetParameters();
                var expressions = new List<Expression>();
                foreach (var item in parameters)
                {
                    var value = args[item.Name];
                    var expression = Expression.Constant(value, item.ParameterType);
                    expressions.Add(expression);
                }
                var body = Expression.TypeAs(Expression.New(constructor, expressions), typeof(object));
                var lambda = Expression.Lambda(body, arg0);
                return lambda.Compile() as Func<Dictionary<string, object>, object>;
            });
        }

        public static T Create<T>(params object[] args)
        {
            var type = typeof(T);
            var values = new Dictionary<string, object>();
            foreach (var arg in args)
            {
                var handler = GetDeconstructionsHandler(arg.GetType());
                handler(arg, values);
            }
            var creator = GetCreatorHandler(type, values);
            return (T)creator(values);
        }

    }
}
