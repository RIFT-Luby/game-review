import { take } from 'rxjs';
import { EventEmitter, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoadingModalComponent } from '../loading-modal.component';

@Injectable({
  providedIn: 'root'
})
export class LoadingModalService {

  closed = new EventEmitter<boolean>();

  constructor(private matDialog: MatDialog) {}

  open(): void {
    const dialog = this.matDialog.open(LoadingModalComponent);

    dialog.afterClosed()
      .pipe(take(1))
      .subscribe((result: boolean) => {
        this.closed.emit(result);
      });
  }

  close(): void {
    const dialog = this.matDialog.closeAll();
  }
}
