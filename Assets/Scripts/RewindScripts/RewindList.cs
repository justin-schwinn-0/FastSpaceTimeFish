using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindList
{
    node first;
    node last;
    int length = 0;

    public void addFrame(RevertFrame r)
    {
        if(first == null)
        {
            first = new node(r);
            last = first;
        }
        else
        {
            first.next = new node(r);
            first.next.prev = first;
            first = first.next;
        }
        length++;
    }

    //discrad frames before a specific game time
    // cullFramesGT(1.0) will cull all frames that are from before 1.0s Game Time
    public void cullFrames(double gameTime)
    {
        while(last != null && last.r.instanceGametime < gameTime)
        {
            cullSingleFrame();
        }
    }
    public void cullSingleFrame()
    {
        if(last != null)
        {
            if(last == first)
            {
                last = null;
                first = null;
                length = 0;
            }
            else
            {
                last.next.prev = null;
                last = last.next;
                length--;
            }
        }
    }

    //returns the revertFrame of the most recent update, to be applied in the calling fucntion
    public RevertFrame popFrame()
    {
        if(first != null)
        {
            node temp = first;
            length--;

            if(first.prev != null)
            {
                first = first.prev;
                first.next = null;
            }
            else 
                first = null;
            return temp.r;

        }
        else return null;
    }

    // pops the list until the last frame that can be poped, and returns the final frame
    public RevertFrame popTil(double Gametime)
    {
        node temp = null;
        while(first != null && first.r.instanceGametime > Gametime)
        {
            temp = first;

            if(first.prev != null)
            {
                first = first.prev;
                first.next = null;
            }
            else 
                first = null;
        }
        
        if(temp == null)
            return null;
        return temp.r;
    }

    public int getlength()
    {
        return length;
    }

    public double getLastTime()
    {
        if(last != null)
        return last.r.instanceGametime;
        return 0;
    }











private class node
{
    public RevertFrame r;
    public node next, prev;

    public node(RevertFrame r)
    {
        this.r = r;
    }
}
}
