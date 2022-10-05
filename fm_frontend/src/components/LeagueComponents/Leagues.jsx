import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Leagues = () => {
    const user = JSON.parse(localStorage.getItem("User"));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("/");
        }
    }, [user, navigate]);

    return <section className='leaguePage container container--pa'>
         <h1 className="title">League Page</h1>
    </section>
}

export default Leagues;