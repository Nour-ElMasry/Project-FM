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

    return <section className="homepage container container--pa">
        <h1 className="title">Home</h1>
        {!user.customer.hasTeam && <CreateTeamLanding />}
        {user.customer.hasTeam && <HomeComponents />}
    </section>
}

export default Home;