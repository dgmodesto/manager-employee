import { Injectable } from "@angular/core";

import { HttpClient } from "@angular/common/http";
import { Observable, Subject } from "rxjs";
import { Employee } from "src/app/models/employee-model";
import { Department } from "../models/department-model";

@Injectable({
  providedIn: "root"
})
export class EmployeeService {
  constructor(private http: HttpClient) {}

  formData: Employee;

  readonly APIUrl = "https://localhost:44344/api";

  getEmpList(): Observable<any> {
    return this.http.get<Employee[]>(this.APIUrl + "/Employee");
  }

  addEmp(emp: Employee) {
    debugger;
    return this.http.post(this.APIUrl + "/Employee", emp);
  }

  deleteEmp(id: number) {
    return this.http.delete(this.APIUrl + "/Employee?id=" + id);
  }

  updateEmp(emp: Employee) {
    return this.http.put(this.APIUrl + "/Employee", emp);
  }

  getDepDropDownValues(): Observable<any> {
    return this.http.get<Department[]>(this.APIUrl + "/Department");
  }

  private _Listenners = new Subject<any>();

  listen(): Observable<any> {
    return this._Listenners.asObservable();
  }

  filter(filterBy: string) {
    return this._Listenners.next(filterBy);
  }
}
