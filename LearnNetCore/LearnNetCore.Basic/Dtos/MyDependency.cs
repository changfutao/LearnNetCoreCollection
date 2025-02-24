namespace LearnNetCore.Basic.Dtos
{
    #region 生命周期
    public interface IMyDependencyTransient
    {
        Guid Id { get; }
    }
    public class MyDependencyTransient : IMyDependencyTransient
    {
        public MyDependencyTransient()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public interface IMyDependencyScoped
    {
        Guid Id { get; }
    }
    public class MyDependencyScoped : IMyDependencyScoped
    {
        public MyDependencyScoped()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public interface IMyDependencySingleton
    {
        Guid Id { get; }
    }
    public class MyDependencySingleton : IMyDependencySingleton
    {
        public MyDependencySingleton()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
    #endregion
    /**
     * 在设计能够纪念性依赖注入的服务
     *  1.避免有状态的、静态类和成员。通过将应用设计为改用单一实例服务,避免创建全局状态
     *  2.避免在服务中直接实例化依赖类。直接实例化会将代码耦合到特定实现
     *  3.不在服务中包含过多内容,确保设计规范,并易于测试
     *  
     * 服务释放
     *  1.容器为其创建的Dispose类型调用IDisposable。 从容器中解析的服务绝对不应由开发人员释放。 如果类型或工厂注册为单一实例，则容器自动释放单一实例。
     *  2.builder.Services.AddSingleton(new Service1()); // 不由服务容器创建的服务,  框架不会自动释放服务,开发人员负责释放服务
     */
    public interface IDependency: IDisposable
    {
        string Id { get; }
    }

    public class DependencyFirst: IDependency
    {
        public DependencyFirst()
        {
            Id = Guid.NewGuid().ToString("N") + " First";
        }
        public string Id { get; }
        public void Dispose()
        {
            Console.WriteLine("DependencyFirst Disposed");
        }
    }

    public class DependencySecond : IDependency
    {
        public DependencySecond()
        {
            Id = Guid.NewGuid().ToString("N") + " Second";
        }
        public string Id { get; }

        public void Dispose()
        {
            Console.WriteLine("DependencySecond Disposed");
        }
    }
}
