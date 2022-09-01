import request from "@/utils/request";

export async function getTeacher(userId){
    return request.get(`/teacher?userId=${userId}`)
}

export async function postSurvey(model){
    return request.post(`/survey`,model)
}

export async function getSurvey(userId){
    return request.get(`/survey?userId=${userId}`,)
}

export async function postQuestion(model){
    return request.post(`/question`,model)
}
export async function getQuestion(surveyId){
    return request.get(`/question?surveyId=${surveyId}`)
}