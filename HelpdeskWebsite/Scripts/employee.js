$(function () { //employee.js
    const getAll = async (msg) => {
        try {
            $('#employeeList').html('<h3>Finding Employee Information, please wait..</h3>');
            let response = await fetch(`api/employees/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildEmployeeList(data, true);
            (msg === '') ? // are we appending to an existing message
                $('#status').text('Employees Loaded') : $('#status').text(`${msg} - Employees Loaded`);
            response = await fetch(`api/departments/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let deps = await response.json();
            localStorage.setItem('alldepartments', JSON.stringify(deps));
        } catch (error) {
            $('#status').text(error.message);
        }
    } //getAll

    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        // tilde below same as emp.Lastname.IndexOf($('#srch).val > 1)
        let filteredData = allData.filter((emp) => ~emp.Lastname.indexOf($('#srch').val()));
        buildEmployeeList(filteredData, false);
    } // filterData

    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need vaild email format"
            }
        }
    });

    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update employee</h4>');

        clearModalFields();
        data.map(employee => {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.Phoneno);
                $('#TextBoxEmail').val(employee.Email);
                $('#ImageHolder').html(`<img height="120" width="110" src="data:image/png;base64,${employee.StaffPicture64}"/>`);
                localStorage.setItem('Id', employee.Id);
                localStorage.setItem('DepartmentId', employee.DepartmentId);
                localStorage.setItem('Timer', employee.Timer);
                localStorage.setItem('StaffPicture', employee.StaffPicture64);
                $('#modalstatus').text('update data');
                loadDepartmentDDL(employee.DepartmentId.toString());
                $('#theModal').modal('toggle');
            } // if
        }); //  data.map
    }   //setupForUpdate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add employee</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new employee');
        loadDepartmentDDL(-1);
        clearModalFields();
    } // setupForAdd

    const loadDepartmentDDL = (empdep) => {
        html = '';
        $('#ddlDeps').empty();
        let alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartments.map(dep => html += `<option value="${dep.Id}">${dep.Name}</option>`);
        $('#ddlDeps').append(html);
        $('#ddlDeps').val(empdep);
    }   //  loadDepartmentDDL



    const clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    } //clearModalFields

    const buildEmployeeList = (data, allEmployees) => {
        $('#employeeList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                <div class= "list-group-item row d-flex text-center" id="heading" style="background-color: gold">
                <div class="col-4 h4">Title</div>
                <div class="col-4 h4">First</div>
                <div class="col-4 h4">Last</div>
            </div>`);
        div.appendTo($('#employeeList'));
        allEmployees ?  localStorage.setItem('allemployees', JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($('#employeeList'));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.Id}" style="background-color: cyan">`);
            btn.html(`<div class="col-4" style="background-color: cyan" id="employeetitle${emp.Id}">${emp.Title}</div>
                        <div class="col-4" style="background-color: cyan" id="employeefname${emp.Id}">${emp.Firstname}</div>
                        <div class="col-4" style="background-color: cyan" id="employeelastname${emp.Id}">${emp.Lastname}</div>`
            );
            btn.appendTo($('#employeeList'));

        }); // map

    } // buildEmployeeList

    const update = async () => {
        try {
            // set up a new client side instance of Employee
            emp = new Object();
            // populate the properties
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            //we stored these 3 earlier
            emp.Id = localStorage.getItem("Id");
            emp.DepartmentId = $('#ddlDeps').val();
            emp.Timer = localStorage.getItem('Timer');
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture')
                : null;
            let response = await fetch('api/employees',  // send the updated back to the server asynchronously usit PUT
                {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(emp)
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
    }   //update

    const add = async () => {
        try {
            // set up a new client side instance of Employee
            emp = new Object();
            // populate the properties
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.DepartmentId = $('#ddlDeps').val();
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture')
                : null;
            emp.Id = -1;
            //send the employee infor to the server asyncchronously using POST
            let response = await fetch('api/employees',  // send the updated back to the server asynchronously usit PUT
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(emp)
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
    } // ADD

    let _delete = async () => {
        try {
            let response = await fetch(`api/employees/${localStorage.getItem('Id')}`,
                {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                });
            if (response.ok) { // or check for response.status
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } // else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    $("#actionbutton").click((e) => {
        if ($("#EmployeeModalForm").valid()) {
            $("#actionbutton").val() === "update" ? update() : add();
        } else {

            $('#modalstatus').text('fix errors');
            e.preventDefault;
        }

    }); // actionbutton click

    $('#srch').keyup(filterData); // srch key press

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete()); // if yes was chosen

    $('#employeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allemployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; // ignore if they clicked onheading or status
        }
    });

    $('input:file').change(() => {
        const reader = new FileReader();
        const file = $('#fileUpload')[0].files[0];

        file ? reader.readAsBinaryString(file) : null;

        reader.onload = (readerEvt) => {
            const binaryString = reader.result;
            const encodedString = btoa(binaryString);
            localStorage.setItem('StaffPicture', encodedString);
        }
    });
   

    

    getAll(''); // first grab the data from server
}); // jQuery ready method