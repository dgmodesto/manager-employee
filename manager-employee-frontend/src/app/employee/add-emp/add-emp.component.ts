import { Component, OnInit } from "@angular/core";

import { MatDialogRef } from "@angular/material";
import { EmployeeService } from "src/app/services/employee.service";
import { NgForm } from "@angular/forms";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: "app-add-emp",
  templateUrl: "./add-emp.component.html",
  styleUrls: ["./add-emp.component.css"]
})
export class AddEmpComponent implements OnInit {
  constructor(
    public dialogbox: MatDialogRef<AddEmpComponent>,
    private service: EmployeeService,
    private snackBar: MatSnackBar
  ) {}

  public listItems: Array<string> = [];

  ngOnInit() {
    this.resetForm();
    this.dropDownRefresh();
  }

  onClose() {
    this.dialogbox.close();

    this.service.filter("Register click");
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    this.service.addEmp(form.value).subscribe(res => {
      debugger;
      this.resetForm(form);
      this.snackBar.open(res.value, "", {
        duration: 5000,
        verticalPosition: "top"
      });
    });
  }

  dropDownRefresh() {
    this.service.getDepDropDownValues().subscribe(data => {
      data.forEach(element => {
        this.listItems.push(element.departmentName);
      });
    });
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }

    this.service.formData = {
      EmployeeID: 0,
      EmployeeName: "",
      Department: "",
      MailID: "",
      DOJ: null
    };
  }
}
