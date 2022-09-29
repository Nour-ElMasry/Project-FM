import { Link } from 'react-router-dom';

const AuthHeader = () => {  
    return <>
        <header>
            <nav className="navbar container">
                <div className="navbar__brand">
                    <Link className="brand_logo flex flex-ai-c" to="/">
                        <img className="brandImg" src="/Images/logo.png" alt="logo"></img>
                    </Link>
                </div>
            </nav>
        </header>
        
    </>;        
}

export default AuthHeader;