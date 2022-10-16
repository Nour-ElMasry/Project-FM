import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import CreateTeamLanding from "./CreateTeamLanding";
import HomeComponents from "./HomeComponents";

const Home = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("/");
        }
    }, [user, navigate]);

    return <section className="homeSection container container--pa">
        <h1 className="title">Home</h1>
        {!user.customer.hasTeam && <CreateTeamLanding user={user} />}
        {user.customer.hasTeam && <HomeComponents user={user}/>}
    </section>
}

export default Home;