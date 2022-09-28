import axios from "axios";

class GeneralAxiosService {
    async getMethod(url){
        return await axios.get(url);
    }

    async getMethodWithParams(url, parameters){
        return await axios.get(url, { params: parameters });
    }

    async postMethod(url, obj){
        return await axios.post(url, obj);
    }

    async deleteMethod(url){ 
        return await axios.delete(url);
    }
}

export default new GeneralAxiosService();