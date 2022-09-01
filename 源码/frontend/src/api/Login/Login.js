import request from "@/utils/request";
export async function login(model){
    return request.post(`/login`,model)
}