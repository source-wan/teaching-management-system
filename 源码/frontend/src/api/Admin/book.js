import request from "@/utils/request";

export async function getBookList(){
    return request.get(`/textbook`)
}


export async function postBook(model){
    return request.post(`/textbook`,model)
}
export async function delBook(BookId){
    return request.delete(`/textbook/${BookId}`)
}
