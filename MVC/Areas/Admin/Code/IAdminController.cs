
using MVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Code
{
    interface IAdminController
    {
        ActionResult Index(string searchString, int page = 1, int pageSize = 10);
        ActionResult Add(object model);
        ActionResult Delete(DeleteViewModel model);
        ActionResult DeleteTransfer(int? id);
        ActionResult UpdateTransfer(int? id);
        ActionResult Update(object model); 
    }
    
}
