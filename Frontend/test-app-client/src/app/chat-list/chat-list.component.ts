import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit {

  title = 'This is a title from the chatlist component';
  persons: Person[] = [];
  newPerson: Person = new Person();

  constructor() { }

  ngOnInit() {

    let person1 = new Person();
    person1.firstName = 'james';
    person1.lastName = 'bond';
    person1.id = 7;

    let person2 = new Person();
    person2.firstName = 'tom';
    person2.lastName = 'cruise';
    person2.id = 2;

    this.persons.push(person1);
    this.persons.push(person2);

  }

  addNewPerson(): void {
    let tempPerson = new Person();
    tempPerson.id = this.newPerson.id;
    tempPerson.firstName = this.newPerson.firstName;
    tempPerson.lastName = this.newPerson.lastName;
    console.log();
    this.persons.push(tempPerson);
    this.newPerson = new Person();
  }
}

class Person {
  firstName: string;
  lastName: string;
  id: number;
}
