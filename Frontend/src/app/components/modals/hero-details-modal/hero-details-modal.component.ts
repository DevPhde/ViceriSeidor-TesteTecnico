import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-hero-details-modal',
  templateUrl: './hero-details-modal.component.html',
  styleUrls: ['./hero-details-modal.component.scss']
})
export class HeroDetailsModalComponent {
  constructor(
    public dialogRef: MatDialogRef<HeroDetailsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  onClose(): void {
    this.dialogRef.close();
  }
}
