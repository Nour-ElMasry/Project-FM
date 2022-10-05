import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const Profile = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("");
        }
    }, [user, navigate]);

    return <section className="profileSection container container--pa">
        <h1 className="title">Profile Page</h1>
    </section>
}
export default Profile;