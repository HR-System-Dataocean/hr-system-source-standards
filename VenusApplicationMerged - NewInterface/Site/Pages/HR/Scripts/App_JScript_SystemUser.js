// JScript File

var strHintMsg;
function ChangePasswordFn()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab  == null)
        return ;
    
    var chkChangePassword           = igtab_getElementById("chkChangePassword",webTab.element);  
    var txtPassword                 = igtab_getElementById("txtPassword",webTab.element);
    var txtConfirmPassword          = igtab_getElementById("txtConfirmPassword",webTab.element);
    var txtOldPassword              = window.document.all.item("UltraWebTab1__ctl0_lblPassword"); //igtab_getElementById("txtOldPassword",webTab.element)
    var txtIsAdmin                  = igtab_getElementById("txtIsAdmin",webTab.element)
    var lblHint                     = igtab_getElementById("lblHint",webTab.element)
    var txtOldPass                  = igtab_getElementById("txtOldPass",webTab.element)
    if (chkChangePassword.checked)
    {
        if (txtIsAdmin.value > 0 )
        {
            txtPassword.disabled        = false;
            txtConfirmPassword.disabled = false; 
        }
        else
        {
            txtPassword.disabled        = false;
            txtOldPass.style.visibility   ="visible"
            lblHint.style.visibility = "visible"
            
        }
        
    }
    else
    {
        txtPassword.value           = ""
        txtConfirmPassword.value    = ""
        txtPassword.disabled        = true;
        txtConfirmPassword.disabled = true;
        lblHint.style.visibility = "hidden"
        txtOldPass.style.visibility = "hidden"
        
    }
    
}

function txtPasswordChange()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab  == null)
        return ;
    
    var chkChangePassword           = igtab_getElementById("chkChangePassword",webTab.element);  
    var txtPassword                 = igtab_getElementById("txtPassword",webTab.element);
    var txtConfirmPassword          = igtab_getElementById("txtConfirmPassword",webTab.element);
    var txtOldPassword = window.document.all.item("UltraWebTab1__ctl0_lblPassword")
    var txtIsAdmin                  = igtab_getElementById("txtIsAdmin",webTab.element)
    var lblHint                     = igtab_getElementById("lblHint",webTab.element)
    var txtOldPass                  = igtab_getElementById("txtOldPass",webTab.element)
    
    if (chkChangePassword.checked)
    {
        if (txtIsAdmin.value == 0)
        {
           if (txtOldPass.value == txtOldPassword.value)
           {
                txtConfirmPassword.disabled = false;
                txtPassword.disabled        = false
                txtPassword.focus()
                txtIsAdmin.value            = 2
                lblHint.style.visibility = "hidden"
                txtOldPass.style.visibility   ="hidden"
           }
           else
           {
                //txtPassword.value           = ""
                txtOldPass.value              = ""
                txtOldPass.focus()
                alert("Invalid Password");
              
           }
        }
        
    }
}



function SetPasswordReadonly()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab  == null)
        return ;
        
    var txtPassword             = igtab_getElementById("txtPassword",webTab.element);
    var txtConfirmPassword      = igtab_getElementById("txtConfirmPassword",webTab.element);
    var lblIsAdmin              = igtab_getElementById("lblIsAdmin",webTab.element);
    var ChkIsAdmin              = igtab_getElementById("ChkIsAdmin",webTab.element);
    var txtIsAdmin              = igtab_getElementById("txtIsAdmin",webTab.element)
    var lblHint                 = igtab_getElementById("lblHint",webTab.element)
    var pnlSecurity             = igtab_getElementById("pnlSecurity",webTab.element)
    var txtOldPass = window.document.all.item("UltraWebTab1__ctl0_lblPassword")
    
    txtPassword.value           = ""
    txtConfirmPassword.value    = ""
    txtPassword.disabled        = true;
    txtConfirmPassword.disabled = true;
    strHintMsg                  = lblHint.innerText
    lblHint.style.visibility = "hidden"
    txtOldPass.style.visibility   ="hidden"
    if (txtIsAdmin.value == 0)
    {
        ChkIsAdmin.disabled  = true
        pnlSecurity.disabled = true
       
    }
    else if (txtIsAdmin.value == 1)
    {
        ChkIsAdmin.disabled = false
        pnlSecurity.disabled = false
    }
    
}