using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using University.BL.Data;

namespace University.API
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //configura el db context para que sea usado como unica instancia por solicitud (singleton).
            app.CreatePerOwinContext(UniversityContext.Create);
        }
    }
}
