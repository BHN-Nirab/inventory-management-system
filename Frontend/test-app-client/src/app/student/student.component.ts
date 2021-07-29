import { StudentService } from './../student.service';
import { Student } from './../models/student';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  students: Student[] = [];
  student: Student = new Student();
  constructor(private studentService: StudentService) { }

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents() {
    this.studentService.getAllStudents().subscribe(
      response => {
        console.log(response);
      }
    );
  }

  insertStudent(): void {
   this.studentService.addStudent(this.student).subscribe(
     response => {
       console.log('data added successfully');
       this.student = new Student();
       this.loadStudents();
     }
   );
  }

}
