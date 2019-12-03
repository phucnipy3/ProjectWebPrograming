using MVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Code
{
    interface IDetailsController<T>
    {
        ActionResult Index(string searchString, int page = 1, int pageSize = 10);
        ActionResult Add(T model);
        ActionResult Delete(DeleteDetailViewModel model);
        ActionResult DeleteTransfer(int? mainID, int? SubID);
        ActionResult UpdateTransfer(int? mainID, int? SubID);
        ActionResult Update(T model);
    }
}
