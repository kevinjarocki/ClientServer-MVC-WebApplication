$(function () { // call.js
    const getAll = async (msg) => {
        try {
            $('#callList').html('<h3>Finding All Previous Calls, please wait...</h3>');
            let response = await fetch(`api/calls/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildCallList(data, true);
            (msg === '')
                ? // are we appending to an existing message
                $('#status').text('Calls Loaded')
                : $('#status').text(`${msg} - Calls Loaded`);
            let responseprobs = await fetch(`api/problems/`);
            if (!responseprobs.ok) // or check for response.status
                throw new Error(`Status - ${responseprobs.status}, Text - ${responseprobs.statusText}`);
            let probs = await responseprobs.json();
            localStorage.setItem('allproblems', JSON.stringify(probs));
            responseemps = await fetch(`api/employees/`);
            if (!responseemps.ok) // or check for response.status
                throw new Error(`Status - ${responseemps.status}, Text - ${responseemps.statusText}`);
            let emps = await responseemps.json();
            localStorage.setItem('allemployees', JSON.stringify(emps));
            responsetechs = await fetch(`api/employees/`);
            if (!responsetechs.ok) // or check for response.status
                throw new Error(`Status - ${responsetechs.status}, Text - ${responsetechs.statusText}`);
            let techs = await responsetechs.json();
            localStorage.setItem('alltechs', JSON.stringify(emps));
        } // try
        catch (error) {
            $('#status').text(error.message);
        } // catch
    } // getAll

    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allcalls'));
        // tilde below same as emp.Lastname.IndexOf($('#srch).val > 1)
        let filteredData = allData.filter((call) => ~call.EmployeeName.indexOf($('#srch').val()));
        buildCallList(filteredData, false);
    } // filterData

    $("#CallModalForm").validate({
        rules: {
            ddlProblem: { required: true },
            ddlEmployee: { required: true },
            ddlTech: { required: true },
            TextAreaNotes: { maxlength: 250, required: true }
        },  //  rules
        errorElement: "div",
        messages: {
            ddlProblem: {
                required: "Problem selection is required."
            },
            ddlEmployee: {
                required: "Employee selection is required."
            },
            ddlTech: {
                required: "Technician selection is required."
            },
            TextAreaNotes: {
                required: "1-250 characters required",
                maxlength: "1 - 250 characters required"
            }
        }   // messages

    }); // validate

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>Update Call</h4>');
        clearModalFields();
        data.map(call => {
            if (call.Id === parseInt(Id)) {
                if (call.OpenStatus == false) {

                    readonlyModalSetup(call);

                }   // if
                else{

                    openModalSetup(call);
                }


                //$('#modalstatus').text('update data');

                $('#theModal').modal('toggle');

            } // if
        }); // data.map
    }   // setupForUpdate

    const closedModalSetup = (call) => {

        $('#labelDateOpened').text(formatDate(call.DateOpened));
        $('#dateOpened').val(formatDate(call.DateOpened));
        $('#labelDateClosed').text(formatDate(call.DateClosed));
        $('#dateClosed').val(formatDate(call.DateClosed));
        $('#TextAreaNotes').val(call.Notes);
        $('#CheckBoxDateClosed').prop('checked', true);
        localStorage.setItem('Id', call.Id);
        localStorage.setItem('ProblemId', call.ProblemId);
        localStorage.setItem('EmployeeId', call.EmployeeId);
        localStorage.setItem('TechId', call.TechId);
        localStorage.setItem('Timer', call.Timer);
        loadProblemDDL(call.ProblemId.toString());
        loadEmployeeDDL(call.EmployeeId.toString());
        loadTechDDL(call.TechId.toString());
        $('#modalstatus').text('view closed call');

    } // closedModalSetup

    const openModalSetup = (call) => {

        $('#ddlProblem').prop('disabled', false);
        $('#ddlEmployee').prop('disabled', false);
        $('#ddlTech').prop('disabled', false);
        $('#CheckBoxDateClosed').prop('disabled', false);
        $('#TextAreaNotes').attr('disabled', false);
        $('#actionbutton').show();

        $('#labelDateOpened').text(formatDate(call.DateOpened));
        $('#dateOpened').val(formatDate(call.DateOpened));
        $('#labelDateClosed').hide();
        $('#dateClosed').hide();
        $('#CheckBoxDateClosed').prop('checked', false);
        $('#TextAreaNotes').val(call.Notes);
        localStorage.setItem('Id', call.Id);
        localStorage.setItem('ProblemId', call.ProblemId);
        localStorage.setItem('EmployeeId', call.EmployeeId);
        localStorage.setItem('TechId', call.TechId);
        localStorage.setItem('Timer', call.Timer);
        loadProblemDDL(call.ProblemId.toString());
        loadTechDDL(call.TechId.toString());
        loadEmployeeDDL(call.EmployeeId.toString());
        $('#modalstatus').text('update open call');
    }       // openModalSetup

    const readonlyModalSetup = (call) => {
        $('#labelDateOpened').text(formatDate(call.DateOpened));
        $('#dateOpened').val(formatDate(call.DateOpened));
        $('#dateClosed').val(formatDate(call.DateClosed));
        $('#labelDateClosed').text(formatDate(call.DateClosed));
        $('#dateClosed').show();
        $('#labelDateClosed').show();
        $('#CheckBoxDateClosed').prop('checked', true);
        $('#TextAreaNotes').val(call.Notes).readOnly = true;
        localStorage.setItem('Id', call.Id);
        localStorage.setItem('ProblemId', call.ProblemId);
        localStorage.setItem('EmployeeId', call.EmployeeId);
        localStorage.setItem('TechId', call.TechId);
        localStorage.setItem('Timer', call.Timer);
        loadProblemDDL(call.ProblemId.toString());
        loadTechDDL(call.TechId.toString());
        loadEmployeeDDL(call.EmployeeId.toString());

        $('#ddlProblem').prop('disabled', true);
        $('#ddlEmployee').prop('disabled', true);
        $('#ddlTech').prop('disabled', true);
        $('#CheckBoxDateClosed').prop('disabled', true);
        $('#TextAreaNotes').attr('disabled', true);
        $('#actionbutton').hide();
        $('#modalstatus').text('delete closed call');
    }      // readonlyModalSetup


    const formatDate = (date) => {
        let d;
        (date === undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth() + 1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min }
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
    } //    formatDate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add call</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new call');
        $('#dateOpened').text(formatDate());
        $('#labelDateOpened').text(formatDate());
        loadTechDDL(-1);
        loadEmployeeDDL(-1);
        loadProblemDDL(-1);
        clearModalFields();
    } // setupForAdd


    const clearModalFields = () => {

        $('#TextAreaNotes').val('');
        $('#dateOpened').val('');
        $('#dateClosed').val('');
        $('#CheckBoxDateClosed').checked = false;
        localStorage.removeItem('Id');
        localStorage.removeItem('TechId');
        localStorage.removeItem('EmployeeId');
        localStorage.removeItem('ProblemId');
        localStorage.removeItem('Timer');
    }   // clearModalFields

    const buildCallList = (data, allCalls) => {
        $('#callList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Call Info</div>
                <div class= "list-group-item row d-flex text-center" id="heading" style="background-color: gold">
                <div class="col-4 h4">Date</div>
                <div class="col-4 h4">For</div>
                <div class="col-4 h4">Problem</div>
            </div>`);
        div.appendTo($('#callList'));
        allCalls ? localStorage.setItem('allcalls', JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add new call</div></button>`);
        btn.appendTo($('#callList'));
        data.map(call => {
            btn = $(`<button class="list-group-item row d-flex" id="${call.Id}" style="background-color: cyan">`);
            btn.html(`<div class="col-4" style="background-color: cyan" id="calldate${call.Id}">${formatDate(call.DateOpened)}</div>
                        <div class="col-4" style="background-color: cyan" id="employeelastname${call.Id}">${call.EmployeeName}</div>
                        <div class="col-4" style="background-color: cyan" id="problemdesc${call.Id}">${call.ProblemDescription}</div>`
            );
            btn.appendTo($('#callList'));

        }); // map
    } // buildEmployeeList

    const loadProblemDDL = (empprob) => {
        html = '';
        $('#ddlProblem').empty();
        let allproblems = JSON.parse(localStorage.getItem('allproblems'));
        allproblems.map(prob => html += `<option value="${prob.Id}">${prob.Description}</option>`);
        $('#ddlProblem').append(html);
        $('#ddlProblem').val(empprob);
    }   //  loadProblemDDL

    const loadTechDDL = (calltech) => {
        html = '';
        $('#ddlTech').empty();
        let alltechs = JSON.parse(localStorage.getItem('alltechs'));

        alltechs.map(tech => {
            if (tech.IsTech == true)
                html += `<option value="${tech.Id}">${tech.Lastname}</option>`
        }); //  map
        $('#ddlTech').append(html);
        $('#ddlTech').val(calltech);
    }   //  loadTechDDL

    const loadEmployeeDDL = (empnames) => {
        html = '';
        $('#ddlEmployee').empty();
        let allemployees = JSON.parse(localStorage.getItem('allemployees'));
        allemployees.map(emp => html += `<option value="${emp.Id}">${emp.Lastname}</option>`);
        $('#ddlEmployee').append(html);
        $('#ddlEmployee').append(empnames);
    }   //  loadEmployeeDDL

    const update = async () => {
        try {
            // set up a new client side instance of a call
            call = new Object();
            // populate properties
            call.Notes = $('#TextAreaNotes').text();
            //  check if the checkbox to close the call is checked.
            // Set OpenStatus to false if the call is closed
            if ($('#CheckBoxDateClosed').checked = true) {
                call.OpenStatus = false;
                call.DateClosed = formatDate();
            }   // if
            //  check if the checkbox to close the call is NOT checked.
            // Set OpenStatus to true if the call is still open
            else if ($('#CheckBoxDateClosed').checked = false) {
                call.OpenStatus = true;
                call.DateClosed = null;
            }
            call.Id = localStorage.getItem("Id");
            call.ProblemId = $('#ddlProblem').val();
            call.TechId = $('#ddlTech').val();
            call.EmployeeId = $('#ddlEmployee').val();
            call.Timer = localStorage.getItem('Timer');
            let response = await fetch('api/calls',     // send the updated back to the server asynchronously using PUT
                {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(call)
                });
            if (response.ok) //check for response.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        } // try/catch
    }   // update


    //  Add a new call object
    const add = async () => {
        try {
            // set up a new client side instance of Call
            call = new Object();
            call.Notes = $('#TextAreaNotes').text();
            call.DateOpened = $('#dateOpened').text();
            call.DateClosed = $('#dateClosed').text();
            call.EmployeeId = $('#ddlEmployee').val();
            call.TechId = $('#ddlTech').val();
            call.ProblemId = $('#ddlProblem').val();
            //  Check if the checkbox is checked. if it is checked then the call is already closed
            if ($('#CheckBoxDateClosed').checked = true) {
                call.OpenStatus = false;
            } else {
                call.OpenStatus = true;
            }
            call.Id = -1;
            let response = await fetch('api/calls',  // send the updated back to the server asynchronously usit PUT
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(call)
                });
            if (response.ok) //check for response.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $('#theModal').modal('toggle');
        }
        catch (error) {
            $('#status').text(error.message);
        } // try/catch
    }   // ADD

    let _delete = async () => {
        try {
            let response = await fetch(`api/calls/${localStorage.getItem('Id')}`,
                {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                });
            if (response.ok) { // or check for response.status
                let data = await response.json();
                getAll(data);
            }   //  if
            else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } // else
            $('#theModal').modal('toggle');
        }   //  try
        catch (error) {
            $('#status').text(error.message);
        }   //  catch
    }   //  _delete

    $('#CheckBoxDateClosed').click(() => {
        if ($('#CheckBoxDateClosed').checked = true) {
            $('#labelDateClosed').text(formatDate());
            $('#dateClosed').val(formatDate());
            $('#labelDateClosed').show();
            $('#dateClosed').show();
        }
        else if ($('#CheckBoxDateClosed').checked = false) {
            $('#labelDateClosed').text('');
            $('#dateClosed').val('');
        }
    }); //  checkBoxCLose

    $("#actionbutton").click((e) => {
        if ($("#CallModalForm").valid()) {
            $("#actionbutton").val() === "update" ? update() : add();
        } else {

            $('#modalstatus').text('fix errors');
            e.preventDefault;
        } //    else
    }); //  actionbutton click

    // complete validation next

    $('#srch').keyup(filterData); // srch key press

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });   //  delete confirmation

    $('#deletebutton').click(() => _delete()); // if yes was chosen

    $('#callList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'callList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allcalls'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; // ignore if they clicked onheading or status
        }
    }); // callList click method

    getAll(''); // grab call data from server
}); // Jquery ready method