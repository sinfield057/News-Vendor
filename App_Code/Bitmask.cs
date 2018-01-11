using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Bitmask
{
    private int basic = 1;
    private int editor = 2;
    private int admin = 4;

    public Bitmask()
    {
        
    }

    public bool checkBasic( int bitmask )
    {
        return ( bitmask & basic ) != 0 ;
    }

    public bool checkEditor(int bitmask)
    {
        return (bitmask & editor) != 0;
    }

    public bool checkAdmin(int bitmask)
    {
        return (bitmask & admin) != 0;
    }

    public int makeBasic(int bitmask)
    {
        return bitmask | basic;
    }

    public int makeEditor(int bitmask)
    {
        return bitmask | editor;
    }

    public int makeAdmin(int bitmask)
    {
        return bitmask | admin;
    }

    public int removeBasic(int bitmask)
    {
        return bitmask & ~basic;
    }

    public int removeEditor(int bitmask)
    {
        return bitmask & ~editor;
    }

    public int removeAdmin(int bitmask)
    {
        return bitmask & ~admin;
    }
}