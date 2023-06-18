import axios from "axios";

class GeneralAxiosService {
  async getMethod(url) {
    const user = JSON.parse(localStorage.getItem("User"));
    var token = "";
    if (user != null) token = user.token;

    return await axios.get(url, {
      headers: { Authorization: "Bearer " + token },
    });
  }

  async getMethodWithParams(url, parameters) {
    const user = JSON.parse(localStorage.getItem("User"));
    var token = "";
    if (user != null) token = user.token;

    return await axios.get(url, {
      params: parameters,
      headers: { Authorization: "Bearer " + token },
    });
  }

  async postMethod(url, obj) {
    const user = JSON.parse(localStorage.getItem("User"));
    var token = "";
    if (user != null) token = user.token;

    return await axios.post(url, obj, {
      headers: { Authorization: "Bearer " + token },
    });
  }

  async putMethod(url, obj) {
    const user = JSON.parse(localStorage.getItem("User"));
    var token = "";
    if (user != null) token = user.token;

    return await axios.put(url, obj, {
      headers: { Authorization: "Bearer " + token },
    });
  }

  async deleteMethod(url) {
    const user = JSON.parse(localStorage.getItem("User"));
    var token = "";
    if (user != null) token = user.token;

    return await axios.delete(url, {
      headers: { Authorization: "Bearer " + token },
    });
  }
}

export default new GeneralAxiosService();
