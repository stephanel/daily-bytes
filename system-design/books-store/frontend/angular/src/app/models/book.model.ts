export interface Book {
    id: number;
    isbn: string;
    title: string;
    authors: Author[],
    language: string;

}

export interface Author
{
    firstName: string;
    lastName: string;
    knownFor: string;
}