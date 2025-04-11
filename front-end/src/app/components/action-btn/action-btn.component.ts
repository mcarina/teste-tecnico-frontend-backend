import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-action-btn',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './action-btn.component.html',
  styleUrls: ['./action-btn.component.css']
})
export class ActionBtnComponent {
  @Input() index: number = 0;
  @Input() user: any;

  menuOpen = false;

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  onEdit() {
    console.log('Editar', this.user);
    this.menuOpen = false;
  }

  onDelete() {
    console.log('Excluir', this.user);
    this.menuOpen = false;
  }
}
