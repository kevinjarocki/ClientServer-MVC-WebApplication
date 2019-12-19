$(function () {
    $('#employeeReportBtn').click(async (e) => {
        try {
            let response = await fetch(`api/employeereport`);
            if (!response.ok)  //  check for response status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json();
            (data === 'report generated')
                ? window.open('/Pdfs/Employee.pdf')
                : $('#lblstatus').text('problem generating report');
        } catch (e) {
            $('#lblstatus').text(e.message);
        }   //try/catch
    }); //  button click
    $('#callReportBtn').click(async (e) => {
        try {
            let response = await fetch(`api/callreport`);
            if (!response.ok)  //  check for response status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json();
            (data === 'report generated')
                ? window.open('/Pdfs/Call.pdf')
                : $('#lblstatus').text('problem generating report');
        } catch (e) {
            $('#lblstatus').text(e.message);
        }   //try/catch
    }); //  button click
}); //  Jquery