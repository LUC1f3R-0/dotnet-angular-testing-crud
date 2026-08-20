import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HomeService } from '../services/home';
import { CrudUserGet } from '../model/crud-user';
import { ConfirmationService, MessageService } from '@openng/optimus-ui/api';
import { ButtonModule } from '@openng/optimus-ui/button';
import { ConfirmTest } from '../confirm-test/confirm-test';

// interface CrudUser {
//   fName: string;
//   lName: string;
//   email: string;
//   age: number;
// };

@Component({
  selector: 'app-home',
  imports: [FormsModule, ReactiveFormsModule, ButtonModule, ConfirmTest],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
  
export class Home implements OnInit{
  users = signal<CrudUserGet[]>([]);
  
  private homeService = inject(HomeService);
  
  private confirmationService = inject(ConfirmationService);
    private messageService = inject(MessageService);
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
      age: [0, [
        Validators.required,
        Validators.min(18),
        Validators.max(99),
      ]]
    })
  }
  
  // crudUsers: CrudUser[] = [];
  
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

    this.homeService.postUser(this.crudApplication.getRawValue()).subscribe({
      next: response => {
        if (response.success) {
          this.homeService.getUsers().subscribe({
            next: response => {
              this.users.set(response.data)
            },
            error: error => {
              console.error('GET failed:', error);
            }
          });
        }
      },
      error: error => {
        console.error('POST failed:', error);
      }
    });
  }
  // remove(value:CrudUser) {
  //   console.log(value);
  //   console.log(this.crudUsers.indexOf(value));
  //   this.crudUsers.splice(this.crudUsers.indexOf(value), 1)
  // }

  
  // user = {
  //   name: 'thushara',
  //   email: '',
  //   isRemember: false
  // }
  
  ngOnInit(): void {
    this.homeService.getUsers().subscribe({
      next: response => {
        const { data, message, success } = response;
        this.users.set(data);
        console.log(data)
      },
      error: error => {
        console.error(error);
      }
    });
  }
  
  deleteUser(uuid: string) {
    // this.homeService.deleteUser(uuid).subscribe({
    //   next: response => {
    //     if(response)
    //     this.homeService.getUsers().subscribe({
    //       next: response => {
    //         // this.users.set(response.data)
    //       },
    //       error: error => {
    //         console.error('GET failed:', error);
    //       }
    //     });
    //   },
    //   error: error => {
    //     const {success, message, data } = error.error;
    //     console.log(message);
    //   }
    // });
  }
  // submit() {
  //   console.log(this.user);
  // }
  // disable() {
  //   this.isDisabled = !this.isDisabled
  //   console.log(this.isDisabled);
  //   this.user.name = '';
  // }

  
  confirm1(event: Event) {
      this.confirmationService.confirm({
        target: event.currentTarget as EventTarget,
        message: 'Do you want to save the changes?',
        header: 'Save Confirmation',
        accept: () => this.messageService.add({ severity: 'success', summary: 'Saved', detail: 'Changes saved successfully' }),
        reject: () => this.messageService.add({ severity: 'info', summary: 'Cancelled', detail: 'Save cancelled' }),
      });
    }
  
  confirm2(event: Event) {
      this.confirmationService.confirm({
        target: event.currentTarget as EventTarget,
        message: 'Are you sure you want to delete?',
        header: 'Delete Confirmation',
        accept: () => this.messageService.add({ severity: 'success', summary: 'Deleted', detail: 'User deleted successfully' }),
        reject: () => this.messageService.add({ severity: 'info', summary: 'Cancelled', detail: 'Delete cancelled' }),
      });
    }
}
