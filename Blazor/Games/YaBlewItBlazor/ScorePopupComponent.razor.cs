using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BasicGamingUIBlazorLibrary.Shells;
using BasicGamingUIBlazorLibrary.Views;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.ViewModels;
using YaBlewItCP.ViewModels;
using YaBlewItBlazor.Views;
using BasicGamingUIBlazorLibrary.BasicControls.SimpleControls;
using BasicGamingUIBlazorLibrary.StartupClasses;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using YaBlewItCP.Data;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.Hands;
using BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.DrawPiles;
using BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.SingleMiscPiles;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BlazorMVVMHelpersLibrary;
using BasicBlazorLibrary.Components.Layouts;

namespace YaBlewItBlazor;

public partial class ScorePopupComponent
{
    [Parameter]
    public EventCallback Close { get; set; }

    private void PrivateClose()
    {
        if (Close.HasDelegate == false)
        {
            return;
        }
        Close.InvokeAsync();
    }
}
