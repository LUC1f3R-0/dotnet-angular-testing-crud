import { ChangeDetectorRef, Component } from '@angular/core';
import { ConfirmationService, MessageService } from '@openng/optimus-ui/api';
import { ConfirmDialogModule } from '@openng/optimus-ui/confirmdialog';
import { DialogModule } from '@openng/optimus-ui/dialog';
import { ToastModule } from '@openng/optimus-ui/toast';

@Component({
  selector: 'app-confirm-test',
  imports: [ConfirmDialogModule, ToastModule, DialogModule],
  templateUrl: './confirm-test.html',
  styleUrl: './confirm-test.css'
})
export class ConfirmTest {

  visible = false;
 
   header = '';
 
   openDialog(header: string) {
     this.header = header;
     this.visible = true;
     this.cd.detectChanges();
   }
 
   closeDialog() {
     this.visible = false;
     this.cd.detectChanges();
   }
  
  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private cd: ChangeDetectorRef
  ) {}

  confirmDelete(event: Event, acceptCallback: () => void) {
    this.confirmationService.confirm({
      target: event.currentTarget as EventTarget,

      message: 'Are you sure you want to delete?',
      header: 'Delete Confirmation',

      accept: () => {
        acceptCallback();

        this.messageService.add({
          severity: 'success',
          summary: 'Deleted',
          detail: 'User deleted successfully'
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

  confirmSave(event: Event, acceptCallback: () => void) {
    this.confirmationService.confirm({
      target: event.currentTarget as EventTarget,

      message: 'Do you want to save the changes?',
      header: 'Save Confirmation',

      accept: () => {
        acceptCallback();

        this.messageService.add({
          severity: 'success',
          summary: 'Saved',
          detail: 'Changes saved successfully'
        });
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
}
