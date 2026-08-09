import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
  
export class Home implements OnInit{

  isDisabled = false;

  crudApplication;
  constructor(private fb: FormBuilder) {
    this.crudApplication = this.fb.group({
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

  onSubmitCrud() {
    if (this.crudApplication.invalid) {
      return;
    }
    console.log(this.crudApplication.value);
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
