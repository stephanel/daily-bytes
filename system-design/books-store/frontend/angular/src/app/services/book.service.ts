import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Book } from "../models/book.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/local";

@Injectable({
    providedIn: 'root'
})
export class BookService {

    baseUrl = `${environment.baseUrl}/books`

    constructor(private httpClient: HttpClient) 
    { }

    get(id: number): Observable<Book> {
        return of<Book>(
        {
            id: 12,
            title: 'Design Patterns',
            isbn: '978-0201633610',
            authors: [
                { firstName: 'Erich', lastName: 'Gamma', knownFor: 'Gang of Four' },
                { firstName: 'Richard', lastName: 'Helm', knownFor: 'Gang of Four' },
                { firstName: 'Ralph', lastName: 'Johnson', knownFor: 'Gang of Four' },
                { firstName: 'John', lastName: 'Vlissides', knownFor: 'Gang of Four' }
            ],
            language: 'English'
        });
    }

    getAll(): Observable<Book[]> {
        return this.httpClient.get<Book[]>(this.baseUrl);
    }
}