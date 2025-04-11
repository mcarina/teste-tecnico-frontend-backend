import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr'
import { ModalDeleteComponent } from '../modal-delete/modal-delete.component';
import { UserService } from '../../services/user.service';
import { ModalUpdateComponent } from '../modal-update/modal-update.component';

@Component({
  selector: 'app-action-btn',
  standalone: true,
  imports: [
    CommonModule,
    ModalDeleteComponent,
    ModalUpdateComponent
  ],
  templateUrl: './action-btn.component.html',
  styleUrls: ['./action-btn.component.css']
})
export class ActionBtnComponent {
  @Input() user: any;
  @Input() index!: number;

  modalType: 'delete' | 'edit' | null = null;

  constructor(
    private userService: UserService,
    private toastService: ToastrService
  ) {}

  menuOpen = false;
  showModal = false;

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  onEdit() {
    this.menuOpen = false;
    this.modalType = 'edit';
    this.showModal = true;
  }

  onDelete() {
    this.menuOpen = false;
    this.modalType = 'delete';
    this.showModal = true;
  }


  confirmUpdate() {
    const updatedData = {
      name: this.user.name,
      email: this.user.email,
    };
  
    this.userService.updateUser(this.user.id, updatedData).subscribe({
      next: () => {
        this.toastService.success('User updated successfully!');
        this.closeModal();
      },
      error: (err:any) => {
        this.toastService.error('Error updating user!');
        console.error('Erro:', err);
        this.closeModal();
      }
    });
  }

confirmDelete() {
  this.userService.deleteUser(this.user.id).subscribe({
    next: () => {
      this.toastService.success('User deleted successfully!');
      this.closeModal();
    },
    error: (err:any) => {
      this.toastService.error('Error deleting user!');
      console.error('Error deleting user', err);
      this.closeModal();
    }
  });
}

cancelUpdate() {
  this.closeModal();
}

cancelDelete() {
  this.closeModal();
}

private closeModal() {
  this.showModal = false;
  this.modalType = null;
}
}
