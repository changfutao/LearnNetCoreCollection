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

    public interface IMyDependency
    {
        Guid Id { get; }
    }

    public class MyDependency: IMyDependency
    {
        public MyDependency()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; }
    }
}
