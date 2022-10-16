import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import ProfileCreateTeam from "./ProfileCreateTeam";
import ProfileHeader from "./ProfileHeader";

const Profile = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("");
        }
    }, [user, navigate]);

    return <section className="profileSection container--pa">
        <h1 className="title">Profile</h1>
        <ProfileHeader user={user.customer}/>
        {!user.customer.hasTeam && <ProfileCreateTeam />}
    </section>
}
export default Profile;