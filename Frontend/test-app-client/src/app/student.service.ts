import { Student } from './models/student';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StudentService {

  private apiUrl = 'http://localhost:5002/api/product/';
  constructor(private http: HttpClient) { }

  public getAllStudents(): Observable<any[]> {
    return this.http.post<any[]>(this.apiUrl + 'getAll', '');
  }

  public addStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(this.apiUrl + 'add', student);
  }

}
