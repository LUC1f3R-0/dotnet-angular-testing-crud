import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

interface CrudUser {
  fName: string;
  lName: string;
  email: string;
  age: number;
};

@Component({
  selector: 'app-home',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
  
export class Home implements OnInit{
  isDisabled = true;

  crudApplication;
  constructor(private fb: FormBuilder) {
    this.crudApplication = this.fb.nonNullable.group({
      firstName: ['', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)
      ]],
      lastName: ['', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.email,
        Validators.required,
        Validators.minLength(5),
        Validators.pattern(/^[^\s@]+@[^\s@]+\.[A-Za-z]{2,}$/),
        Validators.maxLength(100)
      ]],
      age: ['', [
        Validators.required,
        Validators.min(18),
        Validators.max(99),
      ]]
    })
  }

  crudUsers: CrudUser[] = [];
  
  onSubmitCrud() {
    if (this.crudApplication.invalid) {
      return;
    }
  
    const user: CrudUser = {
      fName: this.crudApplication.controls.firstName.value,
      lName: this.crudApplication.controls.lastName.value,
      email: this.crudApplication.controls.email.value,
      age: Number(this.crudApplication.controls.age.value),
    };
  
    if (Number.isNaN(user.age)) {
      return;
    }
    this.crudUsers.unshift(user);
  
    console.log(this.crudUsers);
  }

  remove(value:CrudUser) {
    console.log(value);
    console.log();
    this.crudUsers.splice(this.crudUsers.indexOf(value))
  }



  





  
  
  
  
  
  
  
  user = {
    name: 'thushara',
    email: '',
    isRemember: false
  }
  
  ngOnInit(): void {
    console.log('hello world');
  }

  submit() {
    console.log(this.user);
  }
  disable() { 
    this.isDisabled = !this.isDisabled
    console.log(this.isDisabled);
    this.user.name = '';
  }
}
