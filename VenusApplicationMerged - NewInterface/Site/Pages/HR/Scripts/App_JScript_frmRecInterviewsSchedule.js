var cEnter = 13;
var cTab = 9;
var cF8 = 119;
var CDelete = 46;
var cF9 = 120;
var oActiveCell = null;
var bEnterEditMode = true;
var bExitEditMode = false;
var currIndexRow = -1
var currApplicantID = ""
var currApplicantCode = null
var currInterviewerCode = null
var IsDublicateInter = false
var IsDublicateApp = false
var IsEdit = true
var ApplicantRow
var currPosition = null
var TagretAction = 0
var HDate = ""
var GDate = ""
var HCell
var GCell

        function btnVacationTransaction_Click(oButton, oEvent) {
            var webTab = igtab_getTabById("UltraWebTab1");
            var ddlPosition = window.document.getElementById(GetControlIDFromTab("ddlopenvacancy", webTab)).value
            TagretAction = 1
            PageMethods.GetPosition1(ddlPosition, OnSucceeded, OnFailed)
        }


        function TrimString(str) {
            return str.replace(/^\s+|\s+$/g, '')
        }
        function ShowAlert(strEnglishMsg, strArabicMsg) {
            var language = window.document.getElementById("txtLang").value;
            if (language != "Eng") {
                alert(strArabicMsg)
            }
            else {
                alert(strEnglishMsg)
            }
        }
        function GetControlIDFromTab(controlName, webTab) {
            var control = igtab_getElementById(controlName, webTab.element)
            if (control != null) {
                return control.id
            }
            else {
                control = webTab.findControl(controlName)
                return control.id
            }
        }
        function uwgApplicants_AfterCellUpdateHandler(gridName, cellId) {
            if (IsEdit == true) {
                var grid = igtbl_getGridById(gridName);
                var cell = igtbl_getCellById(cellId);
                var Row = igtbl_getRowById(cellId);
                var rowIndex = Row.getIndex();
                if (cell.Column.Index == 2) {
                    var cellCode = Row.getCellFromKey("Code")
                    var currCodeCell;
                    for (i = 0; i < grid.Rows.length; i++) {
                        row1 = grid.Rows.getRow(i);
                        currCodeCell = row1.getCell(2);
                        if (cellCode.getValue() != null) {
                            if (currCodeCell.getValue() == cellCode.getValue() && rowIndex != i) {
                                IsDublicateApp = true
                            }
                        }
                    }
                }
                if (igtbl_getCellById(cellId).Column.Key == "GStartDate") {
                    GCell = igtbl_getCellById(cellId);
                    HCell = igtbl_getCellById(GCell.NextSibling.id);
                    var GDate = GCell.MaskedValue;
                    PageMethods.Greg2Hijri(GDate, OnSucceeded, OnFailed)
                    IsEdit = false;
                    return;
                }
                if (igtbl_getCellById(cellId).Column.Key == "HStartDate") {

                    HCell = igtbl_getCellById(cellId);
                    GCell = igtbl_getCellById(HCell.PrevSibling.id);
                    var HDate = HCell.MaskedValue;
                    PageMethods.Hijri2Greg(HDate, OnSucceeded, OnFailed);
                    IsEdit = false;
                    return;
                }
            }
            AddRow(gridName, cellId)
        }
        function uwgApplicants_ClickCellButtonHandler(gridName, cellId) {
            var cell = igtbl_getCellById(cellId);
            var Row = cell.getRow();
            var cellCode = Row.getCellFromKey("Code")
            var currPosition = cellCode.getValue()
            if (currPosition != null)
                window.open("frmRecCVOnline.aspx?CVcode=" + currPosition + "", "_Parent", "height=" + 490 + ",width=" + 700 + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=1");
        }
        function uwgApplicants_AfterEnterEditModeHandler(gridName, cellId) {
            var cell = igtbl_getCellById(cellId);
            var Row = cell.getRow();
            var rowindex = Row.getIndex();
            var txtLang = window.document.getElementById("txtLang");
            currApplicantID = Row.getCellFromKey("AID").getValue()
            if ((cell.Column.Key == "Code") || (cell.Column.Key == "GStartDate") || (cell.Column.Key == "HStartDate")) {
                IsEdit = true;
            }
            if (cell.Column.Key == "Code") {
                currApplicantCode = cell.getValue()
            }
            if (bEnterEditMode) {
                var cellEID = Row.getCellFromKey("AID")
                var cellCode = Row.getCellFromKey("Code")
                currApplicantCode = Row.getCellFromKey("Code")
                if (cellCode != null) {
                    if (cellCode.getValue() != null && cellEID.getValue() != null && cellEID.getValue() != 0) {
                        if (TrimString(cellCode.getValue()) != "") {
                            oActiveCell = cell;
                        }
                        else
                            ActivateOnThisCell(cellCode)
                    }
                    else
                        ActivateOnThisCell(cellCode)
                }
            }
        }
        function txtApplicantCode_KeyDown(oEdit, keyCode, oEvent) {
            var cellCode = oActiveCell
            var Row = cellCode.getRow()
            ApplicantRow = cellCode.getRow()
            var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgApplicants")
            var cellEID = Row.getCellFromKey("AID")
            switch (keyCode) {
                case (cTab):
                case (cEnter):
                    {
                        if (cellCode.getValue() != null) {
                            if (TrimString(cellCode.getValue()) != "") {

                                var webTab = igtab_getTabById("UltraWebTab1");
                                var ddlPosition = window.document.getElementById(GetControlIDFromTab("ddlopenvacancy", webTab)).value
                                TagretAction = 0
                                PageMethods.GetPosition(ddlPosition, OnSucceeded, OnFailed)
                            }
                        }
                        else {
                            if (!bExitEditMode) {
                                //ShowAlert("You must enter code", "&#1610;&#1580;&#1576; &#1573;&#1583;&#1582;&#1575;&#1604; &#1575;&#1604;&#1603;&#1608;&#1583;");
                                ShowAlert("You must enter code", "يجب ادخل الكود");
                                ClearRow(Row)
                                ActivateOnThisCell(cellCode)
                            }
                        }
                        break;
                    }
                case (cF9):
                    {
                        var webTab = igtab_getTabById("UltraWebTab1");
                        var ddlPosition = window.document.getElementById(GetControlIDFromTab("ddlopenvacancy", webTab)).value
                        TagretAction = 1
                        PageMethods.GetPosition(ddlPosition, OnSucceeded, OnFailed)
                    }
            }
        }
        function uwgInterviewers_AfterCellUpdateHandler(gridName, cellId) {
            if (IsEdit == true) {
                var grid = igtbl_getGridById(gridName);
                var cell = igtbl_getCellById(cellId);
                var Row = igtbl_getRowById(cellId);
                var rowIndex = Row.getIndex();
                if (cell.Column.Index == 3) {
                    var cellCode = Row.getCellFromKey("Code")
                    var currCodeCell;
                    IsEdit = false;
                    for (i = 0; i < grid.Rows.length; i++) {
                        row1 = grid.Rows.getRow(i);
                        currCodeCell = row1.getCell(3);
                        if (cellCode.getValue() != null) {
                            if (currCodeCell.getValue() == cellCode.getValue() && rowIndex != i) {
                                IsDublicateInter = true
                                return;
                            }
                        }
                    }
                }
            }
            AddRow(gridName, cellId)
        }
        function uwgInterviewers_AfterEnterEditModeHandler(gridName, cellId) {
            var cell = igtbl_getCellById(cellId);
            if (cell.Column.Key != "DefaultEvalType") {
                var Row = cell.getRow();
                var rowindex = Row.getIndex();
                var txtLang = window.document.getElementById("txtLang");
                currApplicantID = Row.getCellFromKey("EID").getValue()
                IsEdit = true;
                if (cell.Column.Key == "Code") {
                    currApplicantCode = cell.getValue()
                }
                if (bEnterEditMode) {
                    var cellEID = Row.getCellFromKey("EID")
                    var cellCode = Row.getCellFromKey("Code")
                    currApplicantCode = Row.getCellFromKey("Code")
                    if (cellCode != null) {
                        if (cellCode.getValue() != null && cellEID.getValue() != null && cellEID.getValue() != 0) {
                            if (TrimString(cellCode.getValue()) != "") {
                                oActiveCell = cell;
                            }
                            else
                                ActivateOnThisCell(cellCode)
                        }
                        else
                            ActivateOnThisCell(cellCode)
                    }
                }
            }
        }
        function txtInterviewerCode_KeyDown(oEdit, keyCode, oEvent) {
            var cellCode = oActiveCell
            var Row = cellCode.getRow()
            ApplicantRow = cellCode.getRow()
            var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgInterviewer")
            var cellEID = Row.getCellFromKey("EID")
            var InterviewerSearch = window.document.getElementById("HiddenField_InterviewerSearch");
            var Lang = window.document.getElementById("txtLang");
            switch (keyCode) {
                case (cTab):
                case (cEnter):
                    {
                        if (cellCode.getValue() != null) {
                            if (TrimString(cellCode.getValue()) != "") {
                                var Args = cellCode.getValue() + "|" + Lang.value
                                PageMethods.GetInterviewerName(Args, OnSucceeded, OnFailed)
                            }
                        }
                        else {
                            if (!bExitEditMode) {
                                //ShowAlert("You must enter code", "&#1610;&#1580;&#1576; &#1573;&#1583;&#1582;&#1575;&#1604; &#1575;&#1604;&#1603;&#1608;&#1583;");
                                ShowAlert("You must enter code", "يجب ادخال الكود");
                                ClearRow2(Row)
                                ActivateOnThisCell(cellCode)
                            }
                        }
                        break;
                    }
                case (cF9):
                    {
                        var winopen = window.open("frmSearchScreen.aspx?TargetControl=" + cellCode.Id + "&SearchID=" + InterviewerSearch.value, "_Parent" + 1, "height=560,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");
                        winopen.document.focus();
                        break;
                    }
            }
        }
        function ClearRow(Row) {
            Row.getCellFromKey('ID').setValue("")
            Row.getCellFromKey('AID').setValue("")
            Row.getCellFromKey('Code').setValue("")
            Row.getCellFromKey('ApplicantName').setValue("")
            Row.getCellFromKey('GStartDate').setValue("")
            Row.getCellFromKey('HStartDate').setValue("")
            Row.getCellFromKey('Houre').setValue("")
            Row.getCellFromKey('E_Mail').setValue("")
        }
        function ClearRow2(Row) {
            Row.getCellFromKey('ID').setValue("")
            Row.getCellFromKey('InterView_ID').setValue("")
            Row.getCellFromKey('EID').setValue("")
            Row.getCellFromKey('Code').setValue("")
            Row.getCellFromKey('InterViewerName').setValue("")
            Row.getCellFromKey('E_Mail').setValue("")
            Row.getCellFromKey('Power').setValue("")
        }
        function ActivateOnThisCell(cell) {
            bEnterEditMode = false;
            cell.activate();
            cell.beginEdit();
            oActiveCell = cell;
            bEnterEditMode = true;
        }

        function AddRow(gridName, cellId) {
            var count = igtbl_getGridById(gridName).Rows.length - 1;
            var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

            if (rowIndex == count) {

                igtbl_addNew(gridName, 0, true, false);

            }
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'Greg2Hijri') {
                var arrValues = result.split("|")
                GCell.setValue(arrValues[0])
                HCell.setValue(arrValues[1])
            }
            if (methodName == 'GetInterviewerName') {
                var cellApplicantName = ApplicantRow.getCellFromKey("InterViewerName")
                var cellID = ApplicantRow.getCellFromKey("EID")
                var cellCode = ApplicantRow.getCellFromKey("Code")
                var cellMail = ApplicantRow.getCellFromKey("E_Mail")
                var cellDefaultEvalType = ApplicantRow.getCellFromKey("DefaultEvalType")

                if (result != "") {
                    if (currApplicantCode != cellCode.getValue())
                        ClearRow2(ApplicantRow)
                    var arrValues = result.split("|")
                    if (IsDublicateInter == false) {
                        cellApplicantName.setValue(arrValues[0])
                        cellID.setValue(arrValues[1])
                        cellCode.setValue(arrValues[2])
                        cellMail.setValue(arrValues[3])

                        var webTab = igtab_getTabById("UltraWebTab1");
                        var ddlEvalType = window.document.getElementById(GetControlIDFromTab("ddlEvalType", webTab)).value
                        if (ddlEvalType != 0) {
                            cellDefaultEvalType.setValue(ddlEvalType)
                        }
                        bEnterEditMode = false;
                        cellMail.activate()
                        oActiveCell = cellMail
                        bEnterEditMode = true;
                    }
                    else {
                        //ShowAlert("This Applicant Added Before", "&#1607;&#1584;&#1575; &#1575;&#1604;&#1605;&#1578;&#1602;&#1583;&#1605; &#1571;&#1590;&#1610;&#1601; &#1605;&#1587;&#1576;&#1602;&#1575;");
                        ShowAlert("This Interviewer Added Before", "تم إضافته سابقا");
                        ClearRow2(ApplicantRow)
                        ActivateOnThisCell(cellCode)
                        IsDublicateInter = false
                    }
                }
                else {
                    //ShowAlert("This Applicant not found", "&#1607;&#1584;&#1575; &#1575;&#1604;&#1605;&#1578;&#1602;&#1583;&#1605; &#1594;&#1610;&#1585; &#1605;&#1608;&#1580;&#1608;&#1583;");
                    ShowAlert("This Interviewer not found", "الكود غير موجود");
                    ClearRow2(ApplicantRow)
                    ActivateOnThisCell(cellCode)
                }
            }
            if (methodName == 'Hijri2Greg') {
                var arrValues = result.split("|")
                GCell.setValue(arrValues[0])
                HCell.setValue(arrValues[1])
            }
            if (methodName == 'GetPosition') {
                currPosition = result
                var ApplicantSearch = window.document.getElementById("HiddenField_ApplicantSearch");
                var Lang = window.document.getElementById("txtLang");
                if (TagretAction == 1) {
                    var Cond = " and IsUsed <> 1 and Position_ID = '" + currPosition + "'"
                    var winopen = window.open("frmSearchScreen.aspx?TargetControl=" + oActiveCell.Id + "&SearchID=" + ApplicantSearch.value + "&Cond=" + Cond, "_Parent" + 1, "height=560,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");
                    winopen.document.focus();
                }
                else {
                    var Args = oActiveCell.getValue() + "|" + Lang.value + "|" + currPosition
                    PageMethods.GetApplicantName(Args, OnSucceeded, OnFailed)
                }
            }
            if (methodName == 'GetPosition1') {
                currPosition = result
                window.open("frmRecApplicantSearch.aspx?Cond=A.IsUsed <> 1 and A.Position_ID = '" + currPosition + "'", "_Parent", "height=" + 490 + ",width=" + 700 + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=0");
            }
            if (methodName == 'GetApplicantName') {
                var cellApplicantName = ApplicantRow.getCellFromKey("ApplicantName")
                var cellID = ApplicantRow.getCellFromKey("AID")
                var cellCode = ApplicantRow.getCellFromKey("Code")
                var cellMail = ApplicantRow.getCellFromKey("E_Mail")
                if (result != "") {
                    if (currApplicantCode != cellCode.getValue())
                        ClearRow(ApplicantRow)
                    var arrValues = result.split("|")
                    if (IsDublicateApp == false) {
                        cellApplicantName.setValue(arrValues[0])
                        cellID.setValue(arrValues[1])
                        cellCode.setValue(arrValues[2])
                        cellMail.setValue(arrValues[3])

                        bEnterEditMode = false;
                        cellMail.activate()
                        oActiveCell = cellMail
                        bEnterEditMode = true;
                    }
                    else {
                        //ShowAlert("This Applicant Added Before", "&#1607;&#1584;&#1575; &#1575;&#1604;&#1605;&#1578;&#1602;&#1583;&#1605; &#1571;&#1590;&#1610;&#1601; &#1605;&#1587;&#1576;&#1602;&#1575;");
                        ShowAlert("This Applicant Added Before", "هذا المتقدم تم اضافته من قبل");
                        ClearRow(ApplicantRow)
                        ActivateOnThisCell(cellCode)
                        IsDublicateApp = false
                    }
                }
                else {
                    //ShowAlert("This Applicant not found", "&#1607;&#1584;&#1575; &#1575;&#1604;&#1605;&#1578;&#1602;&#1583;&#1605; &#1594;&#1610;&#1585; &#1605;&#1608;&#1580;&#1608;&#1583;");
                    ShowAlert("This Applicant not found", "المتقدم غير موجود");
                    ClearRow(ApplicantRow)
                    ActivateOnThisCell(cellCode)
                }
            }
        }
        function OnFailed(error) {
           // alert(error.get_message());
        }
