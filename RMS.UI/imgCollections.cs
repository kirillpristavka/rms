using System;
using System.ComponentModel;
using System.Linq;

namespace RMS.UI
{
    public partial class imgCollections : Component
    {
        public imgCollections()
        {
            InitializeComponent();
        }

        public imgCollections(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
