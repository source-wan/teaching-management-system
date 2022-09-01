import xlsx from 'xlsx'

function readFile(file) {
    return new Promise(resolve => {
        let reader = new FileReader()
        reader.readAsBinaryString(file) // 把当前文件以二进制进行读取
        reader.onload = ev => {
            resolve(ev.target.result)
        }
    })
}

export async function handleExcel(file) {

    let data = await readFile(file)
    let workbook = xlsx.read(data, { type: 'binary' })
    let worksheet = workbook.Sheets[workbook.SheetNames[0]]
    data = xlsx.utils.sheet_to_json(worksheet) // 将数据转换成JSON格式
    return data
}