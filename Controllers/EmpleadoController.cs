using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using web_api_empleado.Models; 
namespace web_api_empleado.Controllers{
    [Route("api/[Controller]")]
public class EmpleadoController : Controller{
    private Conexion dbConexion;
    public EmpleadoController(){
        dbConexion=Conectar.Create();

    }
    [HttpGet]
    public ActionResult Get(){
        return Ok(dbConexion.Empleados.ToArray());
    }
     [HttpGet ("{id}")]
        public ActionResult Get(int id){
            var empleado = dbConexion.Empleados.SingleOrDefault(a => a.id_empleados == id);
            if(empleado!=null){
            return Ok(empleado);
            }
            else{
                return NotFound();
            } 
        }

         [HttpPost]
         public ActionResult Post([FromBody] Empleados empleados){
             if(ModelState.IsValid){
                 dbConexion.Empleados.Add(empleados);
                 dbConexion.SaveChanges();
                 return Ok();
             }
              else{
                return NotFound();
            } 
         }
          //put actualiza los datos  
         [HttpPut]
         public async Task<ActionResult> Put([FromBody] Empleados empleados){
             var v_empleado = dbConexion.Empleados.SingleOrDefault(a => a.id_empleados == empleados.id_empleados);
             if(v_empleado!=null && ModelState.IsValid){
              dbConexion.Entry(v_empleado).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
              return Ok();
             } 
             else{
                 return NotFound();
             } 
         } 
          //Get para elinar datos de la tabla
         [HttpDelete ("{id}")]
          public async Task<ActionResult> Delete(int id){
           var empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleados == id);
           if(empleados!=null){
               dbConexion.Empleados.Remove(empleados);
                 await dbConexion.SaveChangesAsync();
               return Ok();
           }
           else{
              return NotFound();  
           }
          }
   }
}