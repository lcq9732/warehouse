using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public class FormBase : Form
    {
        public FormBase()
        {

        }
        public Form GetForm(string formName)
        {
            Form mdiForm = this.MdiParent;
            if (mdiForm == null)
            {
                mdiForm = this;
            }
            var tmp = mdiForm.MdiChildren.Where(item => item.Name.Equals(formName));
            if (tmp != null && tmp.Count() > 0)
                return tmp.First();
            return null;
        }
        protected void FormResize(Control gridControl, Control bottomControl)
        {
            var heightPoor = bottomControl.Top + bottomControl.Height - this.Height + 50;
            if (Math.Abs(heightPoor) > 10)
            {
                if (gridControl.Name != bottomControl.Name)
                {
                    foreach (Control ctr in this.Controls)
                    {
                        if (ctr.Top > gridControl.Top)
                        {
                            ctr.Top = ctr.Top - heightPoor;
                        }
                    }
                }
                gridControl.Height = gridControl.Height - heightPoor;
            }
            var widthPoor = (this.Width - gridControl.Width) / 2 - gridControl.Left;
            if (Math.Abs(widthPoor) > 10)
            {
                foreach (Control ctr in this.Controls)
                {
                    ctr.Left = ctr.Left + widthPoor;
                }
            }
        }
        public virtual void FormResize()
        {
            
        }
    }
}
