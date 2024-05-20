export interface Book {
    id: number;
    title: string;
    isbn: string;
    authors: Author[];
    language: string;
    thumbnailUrl: string;
}

export interface Author {
    firstName: string;
    lastName: string;
    knownFor: string;
}