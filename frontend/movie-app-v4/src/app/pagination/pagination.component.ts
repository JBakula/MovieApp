import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  @Input() totalPages:number[] = [];
  @Input() currentPath:string = "";
  @Output() pageEvent = new EventEmitter<number>;
  page:number = 1;
  constructor(private router:Router){}
  handlePageChange(p:any){
    this.page = p;
    this.router.navigate([this.currentPath],{ queryParams: { page: this.page }});
    this.pageEvent.emit(this.page);
  }
  handlePrevious(){
    this.page == 1 ? this.page = 1 : this.page = (this.page - 1);
    this.router.navigate([this.currentPath],{ queryParams: { page: this.page }});
    this.pageEvent.emit(this.page);
   }
   handleNext(){
    this.page == this.totalPages.length ? this.page = this.totalPages.length : this.page = (this.page + 1);
    this.router.navigate([this.currentPath],{ queryParams: { page: this.page }});
    this.pageEvent.emit(this.page);
   }
}
