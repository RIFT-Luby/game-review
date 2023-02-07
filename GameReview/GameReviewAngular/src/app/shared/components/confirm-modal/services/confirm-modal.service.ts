import { take } from 'rxjs';
import { EventEmitter, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmModalComponent } from '../confirm-modal.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmModalService {

  closed = new EventEmitter<boolean>();

  constructor(private matDialog: MatDialog) {}

  open(): void {
    const dialog = this.matDialog.open(ConfirmModalComponent);

    dialog.afterClosed()
      .pipe(take(1))
      .subscribe((result: boolean) => {
        this.closed.emit(result);
      });
  }
}
