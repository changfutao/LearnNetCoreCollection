using LearnNetCore.Basic.Dtos;
using LearnNetCore.Basic.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using System.Text;

namespace LearnNetCore.Basic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly MyPosition _myPostion;
        private readonly MyPosition _myRealPosition;

        public HomeController(IOptions<MyPosition> options, IOptionsSnapshot<MyPosition> realOptions)
        {
            _myPostion = options.Value;
            _myRealPosition = realOptions.Value;
        }
        #region 选项
        /*
         * IOptions 和 IOptionsSnapshot 区别:
         * 当配置发生修改时,IOptionsSnapshot 可以获取到修改后的值, 但是IOptions不能, 必须重启应用
         */
        [HttpGet]
        public string GetPosition()
        {
            return _myPostion.Address;
        }

        [HttpGet]
        public string GetRealPosition()
        {
            return _myRealPosition.Address;
        }
        #endregion

        #region 依赖注入
        public string GetTransient([FromServices] IMyDependencyTransient myDependency1, [FromServices] IMyDependencyTransient myDependency2)
        {
            return myDependency1.Id.ToString() + "\r\n" + myDependency2.Id.ToString();
        }

        public string GetScoped([FromServices] IMyDependencyScoped myDependency1, [FromServices] IMyDependencyScoped myDependency2)
        {
            return myDependency1.Id.ToString() + "\r\n" + myDependency2.Id.ToString();
        }

        public string GetSingleton([FromServices] IMyDependencySingleton myDependency1, [FromServices] IMyDependencySingleton myDependency2)
        {
            return myDependency1.Id.ToString() + "\r\n" + myDependency2.Id.ToString();
        }

        // 注册同一服务类型的多个服务实例
        // 如果在IOC中注入同一实现类型的多个实例类型,那么可以通过IEnumerable<实现类型>获取所有的
        public string GetMultiTransient([FromServices] IEnumerable<IDependency> dependencies)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var dependency in dependencies)
            {
                sb.AppendLine(dependency.Id);
            }
            return sb.ToString();
        }
        // 如果在IOC中注入同一实现类型的多个实例类型,那么可以获取单个实现类型时以最后一个注入为准
        public string GetMultiScoped([FromServices] IDependency dependency)
        {
            return dependency.Id;
        }
        #endregion

    }
}
