

select r.regid,c.custname,r.rentdate,r.returndate,r.fee from rent r inner join customer c on r.custid = c.custid where r.rentdate between '4/19/2023' and '4/24/2023'

SELECT SUM(fee) FROM rent;
