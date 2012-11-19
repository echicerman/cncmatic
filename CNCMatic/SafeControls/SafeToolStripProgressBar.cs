using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SafeControls
{
    public class SafeToolStripProgressBar : ToolStripProgressBar
    {
        delegate void SetValue(int value);
        delegate int GetValue();
        delegate void SetMaximum(int value);
        delegate int GetMaximim();

        public int Value
        {
            get
            {
                if ((base.Parent != null) &&        // Make sure that the container is already built
                    (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    GetValue getValueDel = delegate()
                    {
                        return base.Value;
                    };
                    int value = 0;
                    try
                    {
                        // Invoke the SetText operation from the Parent of the ToolStripProgressBar
                        value = (int)base.Parent.Invoke(getValueDel, null);
                    }
                    catch
                    {
                    }

                    return value;
                }
                else
                {
                    return base.Value;
                }
            }

            set
            {
                // Get from the container if Invoke is required
                if ((base.Parent != null) &&        // Make sure that the container is already built
                    (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    SetValue setValueDel = delegate(int val)
                    {
                        base.Value = val;
                    };

                    try
                    {
                        // Invoke the SetText operation from the Parent of the ToolStripProgressBar
                        base.Parent.Invoke(setValueDel, new object[] { value });
                    }
                    catch
                    {
                    }
                }
                else
                    base.Value = value;
            }
        }

        public int Maximo
        {
            get
            {
                if ((base.Parent != null) &&        // Make sure that the container is already built
                   (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    GetMaximim getMax = delegate()
                    {
                        return base.Maximum;
                    };
                    int value = 100;
                    try
                    {
                        // Invoke the SetText operation from the Parent of the ToolStripProgressBar
                        value = (int)base.Parent.Invoke(getMax, null);
                    }
                    catch
                    {
                    }

                    return value;
                }
                else
                {
                    return base.Maximum;
                }
            }
            set
            {
                // Get from the container if Invoke is required
                if ((base.Parent != null) &&        // Make sure that the container is already built
                    (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    SetMaximum _max = delegate(int val)
                    {
                        base.Maximum = val;
                    };

                    try
                    {
                        // Invoke the Maximum operation from the Parent of the ToolStripProgressBar
                        base.Parent.Invoke(_max, new object[] { value });
                    }
                    catch
                    {
                    }
                }
                else
                    base.Maximum = value;

            }
        }
    }

}
