import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  user = {
    name: '',
    email: '',
    isRemember: false
  }
  
  ngOnInit(): void {
    console.log('hello world');
  }

  submit() {
    console.log(this.user);
  }
}
