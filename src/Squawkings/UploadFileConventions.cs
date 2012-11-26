using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoStack.Web.Conventions.Core;

namespace Squawkings
{
    public class UploadFileConventions : HtmlConvention
    {
        public UploadFileConventions()
        {
            this.Inputs.If<HttpPostedFileBase>().Modify((h,r) => h.Attr("type","file"));
        }
    }
}