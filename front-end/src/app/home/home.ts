import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  isDisabled = false;
  
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
