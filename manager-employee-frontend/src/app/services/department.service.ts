import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Department } from "src/app/models/department-model";
import { Observable } from "rxjs";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DepartmentService {
  constructor(private http: HttpClient) {}

  formData: Department;

  readonly APIUrl = "https://localhost:44344/api";

  getDepList(): Observable<Department[]> {
    return this.http.get<Department[]>(this.APIUrl + "/department");
  }

  addDepartment(dep: Department): Observable<{}> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        responseType: "text"
      })
    };
    return this.http.post<String>(
      this.APIUrl + "/Department",
      JSON.stringify(dep),
      httpOptions
    );
  }

  deleteDepartment(id: number) {
    return this.http.delete(this.APIUrl + "/department?" + id);
  }

  updateDepartment(dep: Department) {
    return this.http.put(this.APIUrl + "/department", dep);
  }

  private _listenners = new Subject<any>();
  listen(): Observable<any> {
    return this._listenners.asObservable();
  }

  filter(filterBy: string) {
    this._listenners.next(filterBy);
  }
}
