import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HomeService } from '../services/home';
import { CrudUserGet } from '../model/crud-user';
import { ConfirmationService, MessageService } from '@openng/optimus-ui/api';
import { ButtonModule } from '@openng/optimus-ui/button';
import { ConfirmTest } from '../confirm-test/confirm-test';

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

  @ViewChild('confirm') confirm!: ConfirmTest;
  
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

  onSubmitCrud() {
    if (this.crudApplication.invalid) {
      this.crudApplication.markAllAsTouched();
      return;
    }
  
    const user = this.crudApplication.getRawValue();
  
    if (Number.isNaN(user.age)) {
      return;
    }
  
    this.homeService.postUser(user).subscribe({
      next: response => {
        if (response.success) {
  
          this.messageService.add({
            severity: 'success',
            summary: 'Saved',
            detail: response.message
          });
  
          this.homeService.getUsers().subscribe({
            next: response => {
              this.users.set(response.data);
            },
            error: error => {
              console.error('GET failed:', error);
            }
          });
  
          this.crudApplication.reset();
        }
      },
  
      error: error => {
        this.messageService.add({
          severity: 'error',
          summary: 'Save failed',
          detail: error.error?.message ?? 'Something went wrong'
        });
      }
    });
  }

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

  confirm1(event: Event) {
    this.confirmationService.confirm({
      target: event.currentTarget as EventTarget,
      
      message: 'Do you want to save the changes?',
      header: 'Save Confirmation',
  
      accept: () => {
        this.onSubmitCrud();
      },
  
      reject: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'Cancelled',
          detail: 'Save cancelled'
        });
      }
    });
  }
  
  confirm2(event: Event, uuid: string) {

    this.confirmationService.confirm({
      target: event.currentTarget as EventTarget,

      message: 'Are you sure you want to delete?',
      header: 'Delete Confirmation',

      accept: () => {
        this.homeService.deleteUser(uuid).subscribe({
          next: response => {
            if (response.success) {
              this.messageService.add({
                severity: 'success',
                summary: 'Deleted',
                detail: response.message
              });
              this.users.update(users =>
                users.filter(user => user.uuId !== uuid)
              );
            }
          },
          error: error => {
            this.messageService.add({
              severity: 'error',
              summary: 'Delete failed',
              detail: error.error.message
            });
          }
        });
      },

      reject: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'Cancelled',
          detail: 'Delete cancelled'
        });
      }
    });
  }

  viewUser(event: Event, uuId: string) {
    this.homeService.getUser(uuId).subscribe({
      next: response => {
        const { data, message, success } = response;
        this.confirmationService.confirm({
          target: event.currentTarget as EventTarget,
          
          header: 'User UUID',
          message: `
          <div class="flex flex-col gap-2">
          <div><span class="font-semibold">First Name:</span> ${data.firstName}</div>
          <div><span class="font-semibold">Last Name:</span> ${data.lastName}</div>
          <div><span class="font-semibold">Email:</span> ${data.email}</div>
          <div><span class="font-semibold">Age:</span> ${data.age}</div>
          </div>
          `,
          acceptVisible: false,
          rejectVisible: false
        });
      },
      error: error => {
        console.error(error);
      }
    })
  }

  editUser(event: Event, uuId: string) {
    console.log(uuId);
    this.homeService.getUser(uuId).subscribe({
      next: response => {
        const { data, message, success } = response;
        this.confirmationService.confirm({
          target: event.currentTarget as EventTarget,
          
          header: 'User UUID',
          message: `
          <div class="flex flex-col gap-2">
          <div><span class="font-semibold">First Name:</span> <input value="${data.firstName}"/></div>
          <div><span class="font-semibold">Last Name:</span> ${data.lastName}</div>
          <div><span class="font-semibold">Email:</span> ${data.email}</div>
          <div><span class="font-semibold">Age:</span> ${data.age}</div>
          </div>
          `,
        });
      },
      error: error => {
        console.error(error);
      }
    })
  }
}
