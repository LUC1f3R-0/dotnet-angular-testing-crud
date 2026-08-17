import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HomeService } from '../services/home';

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
  private homeService = inject(HomeService);
  
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
  
    // const user: CrudUser = {
    //   fName: this.crudApplication.controls.firstName.value,
    //   lName: this.crudApplication.controls.lastName.value,
    //   email: this.crudApplication.controls.email.value,
    //   age: Number(this.crudApplication.controls.age.value),
    // };
  
    if (Number.isNaN(this.crudApplication.controls.age.value)) {
      return;
    }
    // this.crudUsers.unshift(user);
  
    console.log(this.crudApplication.value);
  }
  // remove(value:CrudUser) {
  //   console.log(value);
  //   console.log(this.crudUsers.indexOf(value));
  //   this.crudUsers.splice(this.crudUsers.indexOf(value), 1)
  // }



  





  
  
  
  
  
  
  
  user = {
    name: 'thushara',
    email: '',
    isRemember: false
  }
  
  ngOnInit(): void {
    this.homeService.getPokemon().subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.error(error);
      }
    });
  }

  // submit() {
  //   console.log(this.user);
  // }
  // disable() { 
  //   this.isDisabled = !this.isDisabled
  //   console.log(this.isDisabled);
  //   this.user.name = '';
  // }
}
