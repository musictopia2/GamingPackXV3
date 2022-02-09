using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahJongSolitaireCP.Data;
internal class CloningContext : MainContext
{
    protected override void Configure(ICustomConfig config)
    {
        config.Make<BasicList<BoardInfo>>(x =>
        {
            x.Cloneable(false, x =>
            {

            });
        });
    }
}