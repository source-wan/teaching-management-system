import request from "@/utils/request";
//院系
export async function getAcademy(){
    return request.get(`/academy`)
}

//新增院系
export async function postAcademy(model){
    return request.post(`/academy`,model)
}
//删除院系
export async function delAcademy(id){
    return request.delete(`/academy/${id}`,)
}
//查找院系
export async function searchAcademy(AcademyName){
return request.get(`/academy?AcademyName=${AcademyName}`)
}
//专业
export async function getMajor(AcademyId){
    return request.get(`/major?AcademyId=${AcademyId}`)
}
export async function delMajor(id){
    return request.delete(`/major/${id}`,)
}
//新增专业
export async function postMajor(model){
    return request.post("/major",model)
}


//校区
export async function getCampus(campusId){
    return request.get(`/campus/${campusId}`)
}

export async function getCampusList(){
    return request.get(`/campus`)
}